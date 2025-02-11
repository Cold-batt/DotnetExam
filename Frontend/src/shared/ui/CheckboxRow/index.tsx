import * as Checkbox from '@radix-ui/react-checkbox';
import clsx from 'clsx';
import { FC, ForwardedRef, forwardRef } from 'react';

import { TextBox } from '../TextBox';

import CheckboxOffIcon from '@/assets/icons/checkbox-off.svg?svgr';
import CheckboxOnIcon from '@/assets/icons/checkbox-on.svg?svgr';

import styles from './CheckboxRow.module.scss';

type CheckboxRowProps = {
  title: string;
  value?: boolean;
  onChange: (val: boolean) => void;
  fontSize?: '12' | '16';
};

const CheckboxRow: FC<CheckboxRowProps> = forwardRef(
  ({ title, value = false, onChange, fontSize = '12' }, ref: ForwardedRef<HTMLButtonElement>) => {
    return (
      <Checkbox.Root
        key={title}
        checked={value}
        className={clsx(styles.root)}
        onCheckedChange={onChange}
        ref={ref}
      >
        <div className={styles.checkboxWrapper}>
          <CheckboxOffIcon className={styles.offIcon} />
          <Checkbox.Indicator className={styles.indicator}>
            <CheckboxOnIcon />
          </Checkbox.Indicator>
        </div>
        <TextBox variant={fontSize} color='black' align='left'>
          {title}
        </TextBox>
      </Checkbox.Root>
    );
  },
);

export { CheckboxRow };
