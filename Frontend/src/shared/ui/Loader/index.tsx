import clsx from 'clsx';
import { FC } from 'react';

import { TextProps } from '../TextBox';

import LoaderIcon from '@/assets/icons/loader.svg?svgr';

import styles from './Loader.module.scss';

interface LoaderProps {
  color?: TextProps['color'];
  size?: 'small' | 'big';
}

const Loader: FC<LoaderProps> = ({ color = 'black', size = 'small' }) => {
  return (
    <div className={styles.root}>
      <LoaderIcon
        style={{
          color: `var(--${color})`,
        }}
        className={clsx(styles.loader, styles[size])}
      />
    </div>
  );
};

export { Loader };
