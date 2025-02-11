import clsx from 'clsx';
import { ReactNode, useState } from 'react';
import ReactSelect from 'react-select';

import { SelectStyles } from '@/shared/constants';

import { TextBox } from '../TextBox';

import CheckIcon from '@/assets/icons/check.svg?svgr';

import styles from './Select.module.scss';

type OptionType<T> = {
  value: T;
  label: string;
};

type SelectType<T> = {
  iconLeft?: ReactNode;
  items?: OptionType<T>[];
  value: T | undefined;
  onChange: (value: T | undefined) => void;
  withCheckIcon?: boolean;
  label?: string;
  placeholder?: string;
  onSearchChange?: (search: string) => void;
};

const Option = (props: any) => {
  const { data, isSelected, innerRef, innerProps, withCheckIcon } = props;

  return (
    <div ref={innerRef} {...innerProps} className={styles.option}>
      <TextBox variant='16' color='black'>
        {data.label}
      </TextBox>
      {isSelected && withCheckIcon && <CheckIcon />}
    </div>
  );
};

const Select = <T,>({
  iconLeft,
  items,
  label,
  value,
  onChange,
  withCheckIcon = false,
  placeholder = '',
  onSearchChange,
}: SelectType<T>) => {
  const [search, setSearch] = useState('');
  const [focus, setFocus] = useState(false);

  const customComponents = {
    DropdownIndicator: () => null,
    IndicatorSeparator: () => null,
    Option: (props: any) => Option({ ...props, withCheckIcon }),
  };

  const handeInputChange = (value: string) => {
    setSearch(value);
    if (onSearchChange) {
      onSearchChange(value);
    }
  };

  return (
    <div className={styles.root}>
      <div className={styles.row}>
        {iconLeft && <div className={styles.icon}>{iconLeft}</div>}
        <div className={styles.wrapper}>
          {label && (
            <label
              className={clsx(styles.label, {
                [styles.activeLabel]: focus || !!value || placeholder,
                [styles.labelWithPlaceholder]: placeholder,
              })}
            >
              {label}
            </label>
          )}
          <ReactSelect<OptionType<T>>
            styles={SelectStyles}
            inputValue={search}
            onInputChange={handeInputChange}
            value={
              {
                // @ts-ignore
                label: value?.name,
                value: value,
              } as OptionType<T>
            }
            options={items}
            placeholder={placeholder}
            onFocus={() => setFocus(true)}
            onBlur={() => setFocus(false)}
            noOptionsMessage={() => <></>}
            onChange={(option) => {
              onChange(option?.value);
            }}
            components={customComponents}
            isSearchable
            isClearable={false}
          />
        </div>
      </div>
    </div>
  );
};

export { Select };
