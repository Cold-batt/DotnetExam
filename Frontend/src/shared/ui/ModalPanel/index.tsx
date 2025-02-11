import * as Dialog from '@radix-ui/react-dialog';
import clsx from 'clsx';
import { FC, ReactNode } from 'react';

import XMarkIcon from '@/assets/icons/xmark.svg?svgr';

import styles from './ModalPanel.module.scss';

type ModalProps = {
  open?: boolean;
  setOpen?: (value: boolean) => void;
  triggerContent?: ReactNode;
  children: ReactNode;
  zIndex?: number;
  gap?: number;
  fullScreen?: boolean;
};

const ModalPanel: FC<ModalProps> = ({
  open,
  setOpen,
  triggerContent,
  children,
  gap,
  zIndex = 100,
  fullScreen = false,
}) => {
  return (
    <Dialog.Root open={open} onOpenChange={setOpen}>
      {!!triggerContent && (
        <Dialog.Trigger className={clsx(styles.trigger)}>{triggerContent}</Dialog.Trigger>
      )}

      <Dialog.Portal>
        <Dialog.Overlay
          className={styles.overlay}
          style={{
            zIndex: zIndex - 1,
          }}
        />
        <div className={styles.container} style={{ zIndex }}>
          <Dialog.Content
            className={clsx(fullScreen ? styles.fullScreenContent : styles.modalContent)}
          >
            {!fullScreen && (
              <Dialog.Close className={styles.close} onClick={() => setOpen?.(false)}>
                <XMarkIcon />
              </Dialog.Close>
            )}
            <div
              style={{
                gap: `${gap}px`,
              }}
              className={clsx(styles.overflow, {
                [styles.content]: !fullScreen,
              })}
            >
              {children}
            </div>
          </Dialog.Content>
        </div>
      </Dialog.Portal>
    </Dialog.Root>
  );
};

export { ModalPanel };
