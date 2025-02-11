import clsx from 'clsx';
import { FC, ReactNode } from 'react';
import { Link } from 'react-router-dom';

import { TextBox } from '../TextBox';

import styles from './DropdownMenu.module.scss';

export interface DropdownMenuItemProps {
  className?: string;
  children: ReactNode;
  iconLeft?: ReactNode;
  iconRight?: ReactNode;
  onClick?: () => void;
  href?: string;
}

const DropdownMenuItem: FC<DropdownMenuItemProps> = ({
  className,
  children,
  iconLeft,
  iconRight,
  href,
  onClick,
}) => {
  const classes = clsx(className, styles.item);

  if (href)
    return (
      <TextBox className={classes} onClick={onClick} to={href} as={Link}>
        {iconLeft}
        <span className={styles.itemText}>{children}</span>
        {iconRight}
      </TextBox>
    );

  return (
    <TextBox
      className={classes}
      onClick={onClick}
      {...(onClick ? { as: 'button', type: 'button' } : {})}
    >
      {iconLeft}
      <span className={styles.itemText}>{children}</span>
      {iconRight}
    </TextBox>
  );
};

export default DropdownMenuItem;
