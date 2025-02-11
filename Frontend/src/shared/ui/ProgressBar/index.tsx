import * as Progress from '@radix-ui/react-progress';
import { FC } from 'react';

import styles from './ProgressBar.module.scss';

type ProgressBarProps = {
  value: number;
};

const ProgressBar: FC<ProgressBarProps> = ({ value }) => {
  return (
    <Progress.Root className={styles.root} value={value}>
      <Progress.Indicator
        className={styles.indicator}
        style={{ transform: `translateX(-${100 - value}%)` }}
      />
    </Progress.Root>
  );
};

export default ProgressBar;
