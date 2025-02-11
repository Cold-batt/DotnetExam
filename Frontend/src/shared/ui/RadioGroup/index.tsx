import * as RadioGroupProps from '@radix-ui/react-radio-group';
import clsx from 'clsx';
import { FC, ForwardedRef, forwardRef } from 'react';

import { RadioItem } from '@/shared/types';

import { TextBox } from '../TextBox';

import RadioOffIcon from '@/assets/icons/radio-off.svg?svgr';
import RadioOnIcon from '@/assets/icons/radio-on.svg?svgr';

import styles from './RadioGroup.module.scss';

type RadioGroupType = {
  items: RadioItem[];
  value?: string;
  onChange: (value: string) => void;
  direction?: 'row' | 'column';
};

const RadioGroup: FC<RadioGroupType> = forwardRef(
  ({ items, value, onChange, direction = 'row' }, ref: ForwardedRef<HTMLDivElement>) => {
    return (
      <RadioGroupProps.Root
        className={clsx(styles.root, {
          [styles.vertical]: direction === 'column',
        })}
        value={value}
        ref={ref}
      >
        {items.map((item) => (
          <div key={item.label} className={styles.item}>
            <RadioGroupProps.Item
              className={styles.radioGroupItem}
              value={item.value}
              id={item.label}
              onClick={() => onChange(item.value)}
              disabled={item.disabled}
            >
              <RadioOffIcon className={styles.offIcon} />
              <RadioGroupProps.Indicator className={styles.radioGroupIndicator}>
                <RadioOnIcon />
              </RadioGroupProps.Indicator>
            </RadioGroupProps.Item>
            <label htmlFor={item.label}>
              <TextBox variant='16' color='black'>
                {item.label}
              </TextBox>
            </label>
          </div>
        ))}
      </RadioGroupProps.Root>
    );
  },
);

export { RadioGroup };
