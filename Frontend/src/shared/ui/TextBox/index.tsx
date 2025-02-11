import clsx from 'clsx';
import { ElementType } from 'react';

import styles from './TextBox.module.scss';

export interface TextProps {
  variant?: '60' | '34' | '32' | '24' | '18' | '16' | '14' | '12' | '10';
  fontWeight?: '400' | '500' | '700';
  color?:
    | 'black'
    | 'greyOne'
    | 'red'
    | 'white'
    | 'inherit'
    | 'green'
    | 'nativeWhite'
    | 'nativeBlack'
    | 'orange';
  align?: 'center' | 'left' | 'right';
  nowrap?: boolean;
}

const TextBox = <T extends ElementType = 'p'>({
  variant = '12',
  className,
  fontWeight = '400',
  as,
  color = 'black',
  nowrap,
  align,
  ...props
}: PolymorphicProps<T, TextProps>) => {
  const Tag = as ?? 'p';
  return (
    <Tag
      style={{
        fontWeight,
        color: `var(--${color})`,
      }}
      className={clsx(className, styles.root, styles['f' + variant], styles[align ?? ''], {
        [styles.nowrap]: nowrap,
      })}
      {...props}
    />
  );
};

export { TextBox };
