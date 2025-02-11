import clsx from 'clsx';
import { ReactNode, useState } from 'react';
import ReactSelect from 'react-select';

import { SelectStyles } from '@/shared/constants';

import { TextBox } from '../TextBox';

import styles from './Select.module.scss';

type OptionType<T> = {
  value: T;
  label: string;
};

type MultiSelectType<T> = {
  iconLeft?: ReactNode;
  items?: OptionType<T>[];
  value: T[] | undefined;
  onChange: (val: T[]) => void;
  withCheckIcon?: boolean;
  label?: string;
  recomended?: OptionType<T>[];
  onInputChange?: (val: string) => void;
  placeholder?: string;
  iconRight?: ReactNode;
  fetchNextPage?: () => void;
  isLoading?: boolean;
};

const Option = (props: any) => {
  const { data, innerRef, innerProps } = props;

  return (
    <div ref={innerRef} {...innerProps} className={styles.option}>
      <TextBox variant='16' color='black'>
        {data.label}
      </TextBox>
    </div>
  );
};

const MultiValueContainer = ({ selectProps, data }: any) => {
  const values = selectProps.value;
  if (values) {
    return values[values.length - 1].label === data.label ? data.label : data.label + ', ';
  } else return '';
};

const MultiSelect = <T,>({
  iconLeft,
  items,
  label,
  value,
  onChange,
  recomended,
  placeholder = '',
  iconRight,
  onInputChange,
  fetchNextPage,
  isLoading,
}: MultiSelectType<T>) => {
  const [search, setSearch] = useState('');
  const [focus, setFocus] = useState(false);

  const customComponents = {
    DropdownIndicator: () => null,
    IndicatorSeparator: () => null,
    Option,
    MultiValueRemove: () => null,
    MultiValueContainer,
  };

  const handleChangeSearch = (val: string) => {
    setSearch(val);
    if (onInputChange) {
      onInputChange?.(val);
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
                [styles.activeLabel]: focus || [...(value ?? [])].length > 0 || placeholder,
                [styles.labelWithPlaceholder]: placeholder,
              })}
            >
              {label}
            </label>
          )}
          <ReactSelect<OptionType<T>, true>
            styles={SelectStyles}
            inputValue={search}
            onInputChange={handleChangeSearch}
            value={value?.map((val) => ({
              value: val,
              label: `${val}`,
            }))}
            options={items}
            placeholder={placeholder}
            onFocus={() => setFocus(true)}
            onBlur={() => setFocus(false)}
            noOptionsMessage={() => <></>}
            onChange={(options) => onChange(options.map((option) => option.value))}
            components={customComponents}
            onMenuScrollToBottom={fetchNextPage}
            isSearchable
            isMulti={true}
            isClearable={false}
            isLoading={isLoading}
          />
          {iconRight && <div className={styles.iconRight}>{iconRight}</div>}
        </div>
      </div>
      <div className={styles.recomended}>
        {!!recomended &&
          recomended.map((item, i) => (
            <TextBox
              key={i}
              onClick={() => {
                const preparedValue = [...(value ?? [])];

                if (preparedValue.includes(item.value)) {
                  return onChange(preparedValue.filter((val) => val !== item.value));
                } else {
                  return onChange([...preparedValue, item.value]);
                }
              }}
              variant='12'
              color={value?.includes(item.value) ? 'black' : 'greyOne'}
            >
              #{item.label}
            </TextBox>
          ))}
      </div>
    </div>
  );
};

export { MultiSelect };
