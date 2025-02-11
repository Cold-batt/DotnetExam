import { FC } from 'react';

import { TextBox } from '../TextBox';

import styles from './Separator.module.scss';

type SeparatorType = {
  text?: string;
  direction?: 'horizontal' | 'vertical';
};

const Separator: FC<SeparatorType> = ({ text, direction = 'horizontal' }) => {
  return text ? (
    <div className={styles.verticalContainer}>
      <div className={styles[direction]} />
      <TextBox variant='12' color='greyOne'>
        {text}
      </TextBox>
      <div className={styles[direction]} />
    </div>
  ) : (
    <div className={styles[direction]} />
  );
};

export { Separator };
