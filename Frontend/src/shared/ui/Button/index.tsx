import clsx from 'clsx';
import { ButtonHTMLAttributes, DetailedHTMLProps, FC } from 'react';

import { Loader } from '../Loader';
import { TextBox, TextProps } from '../TextBox';

import styles from './Button.module.scss';

type ButtonProps = {
  variant?: 'primary' | 'secondary' | 'ghost' | 'red';
  size?: 'large' | 'small' | 'icon' | 'null';
  wFull?: boolean;
  color?: TextProps['color'];
  fontSize?: TextProps['variant'];
  isLoading?: boolean;
} & DetailedHTMLProps<ButtonHTMLAttributes<HTMLButtonElement>, HTMLButtonElement>;

const Button: FC<ButtonProps> = ({
  variant = 'primary',
  size = 'large',
  type = 'button',
  color,
  wFull,
  className,
  fontSize,
  isLoading,
  ...rest
}) => {
  return (
    <TextBox
      as='button'
      type={type}
      variant={fontSize}
      className={clsx(styles.container, styles[variant], styles[size], className, {
        [styles.disabledPrimary]: rest.disabled && variant === 'primary',
        [styles.disabledSecondary]: rest.disabled && variant === 'secondary',
        [styles.wFull]: wFull,
      })}
      color={color ? color : variant === 'primary' ? 'white' : 'black'}
      {...rest}
      children={
        isLoading ? (
          <Loader color={color ? color : variant === 'primary' ? 'white' : 'black'} />
        ) : (
          rest.children
        )
      }
    />
  );
};

export { Button };
