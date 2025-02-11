import * as Checkbox from '@radix-ui/react-checkbox';
import clsx from 'clsx';
import { FC } from 'react';

import { TextBox } from '../TextBox';

import CheckIcon from '@/assets/icons/check.svg?svgr';

import styles from './CheckRow.module.scss';

type CheckRowProps = {
  title: string;
  isChecked: boolean;
  changeChecked: (checked: Checkbox.CheckedState) => void;
};

const CheckRow: FC<CheckRowProps> = ({ title, isChecked, changeChecked }) => {
  return (
    <Checkbox.Root key={title} checked={isChecked} className={clsx(styles.root)} onCheckedChange={(checked) => changeChecked(checked)}>
      <TextBox variant='16' color='black' align='left'>
        {title}
      </TextBox>
      <Checkbox.Indicator className={styles.indicator}>
        <CheckIcon />
      </Checkbox.Indicator>
    </Checkbox.Root>
  );
};

export { CheckRow };
