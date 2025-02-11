import * as Dialog from '@radix-ui/react-dialog';
import clsx from 'clsx';
import { FC, ReactNode } from 'react';

import { BaseModalProps } from '@/shared/types';

import { TextBox } from '../TextBox';

import XMarkIcon from '@/assets/icons/xmark.svg?svgr';

import styles from './ModalWindow.module.scss';

type ModalProps = {
  triggerContent?: ReactNode;
  children: ReactNode;
  title?: string;
  description?: string;
  zIndex?: number;
} & BaseModalProps;

const ModalWindow: FC<ModalProps> = ({
  open,
  setOpen,
  triggerContent,
  children,
  title,
  description,
  zIndex = 100,
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
          <Dialog.Content className={clsx(styles.modalContent)}>
            <Dialog.Close className={styles.close} onClick={() => setOpen?.(false)}>
              <XMarkIcon />
            </Dialog.Close>
            {(title || description) && (
              <div className={styles.title}>
                {title && (
                  <TextBox variant='18' color='black' align='center'>
                    {title}
                  </TextBox>
                )}
                {description && (
                  <TextBox variant='14' color='black' align='center'>
                    {description}
                  </TextBox>
                )}
              </div>
            )}
            {children}
          </Dialog.Content>
        </div>
      </Dialog.Portal>
    </Dialog.Root>
  );
};

export { ModalWindow };
