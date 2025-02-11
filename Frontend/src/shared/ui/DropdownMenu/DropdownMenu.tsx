import * as Popover from '@radix-ui/react-popover';
import clsx from 'clsx';
import { FC, ReactNode, useLayoutEffect, useState } from 'react';

import styles from './DropdownMenu.module.scss';

export interface DropdownMenuProps extends Popover.PopoverContentProps {
  className?: string;
  dropdownClassName?: string;
  trigger: ReactNode;
  children: ReactNode;
  keepOpen?: boolean;
  noPadding?: boolean;
  fullWidth?: boolean;
  open?: boolean;
  defaultOpen?: boolean;
  onOpenChange?: (value: boolean) => void;
}

const DropdownMenu: FC<DropdownMenuProps> = ({
  className,
  dropdownClassName,
  trigger,
  children,
  keepOpen,
  noPadding,
  fullWidth,
  open,
  defaultOpen,
  onOpenChange,
  ...props
}) => {
  const [internalOpen, setInternalOpen] = useState(false);

  const closeMenu = () => setInternalOpen(false);
  const handleOpenChange = (value: boolean) => {
    if (open === undefined) setInternalOpen(value);

    onOpenChange?.(value);
  };

  useLayoutEffect(() => {
    if (open !== undefined) setInternalOpen(open);
    if (open === undefined && defaultOpen !== undefined) setInternalOpen(defaultOpen);
  }, [defaultOpen, open]);

  return (
    <Popover.Root open={internalOpen} onOpenChange={handleOpenChange}>
      <Popover.Trigger asChild>{trigger}</Popover.Trigger>
      <Popover.Portal>
        <Popover.Content
          onCloseAutoFocus={(event) => event.preventDefault()}
          className={clsx(dropdownClassName, styles.popover, {
            [styles.noPadding]: noPadding,
            [styles.fullWidth]: fullWidth,
          })}
          sideOffset={10}
          align='start'
          {...props}
        >
          <div
            className={clsx(styles.content, className)}
            onClick={!keepOpen ? closeMenu : undefined}
          >
            {children}
          </div>
        </Popover.Content>
      </Popover.Portal>
    </Popover.Root>
  );
};

export default DropdownMenu;
