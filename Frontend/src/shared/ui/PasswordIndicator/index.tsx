import clsx from 'clsx';
import { FC } from 'react';

import { TextBox } from '../TextBox';

import CheckIcon from '@/assets/icons/check.svg?svgr';
import DotIcon from '@/assets/icons/dot.svg?svgr';

import styles from './PasswordIndicator.module.scss';

type PasswordIndicatorType = {
  password: string;
};

const requirements = [
  {
    label: '8 character minimum',
    test: (pwd: string) => !!pwd && pwd?.length >= 8,
  },
  {
    label: 'one uppercase letter',
    test: (pwd: string) => !!pwd && /[A-Z]/.test(pwd),
  },
  {
    label: 'one lowercase letter',
    test: (pwd: string) => !!pwd && /[a-z]/.test(pwd),
  },
  {
    label: 'one number',
    test: (pwd: string) => !!pwd && /[0-9]/.test(pwd),
  },
];

const PasswordIndicator: FC<PasswordIndicatorType> = ({ password }) => {
  return (
    <div className={styles.root}>
      {requirements.map((req, index) => (
        <div
          key={index}
          className={clsx(styles.item, req.test(password) ? styles.passed : styles.failed)}
        >
          {req.test(password) ? <CheckIcon /> : <DotIcon />}
          <TextBox variant='10' color='inherit'>
            {req.label}
          </TextBox>
        </div>
      ))}
    </div>
  );
};

export { PasswordIndicator };
