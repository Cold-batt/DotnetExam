import { ButtonHTMLAttributes, DetailedHTMLProps, FC, ReactNode } from 'react';

import { Button } from '../Button';
import { TextBox, TextProps } from '../TextBox';

import Chevron from '@/assets/icons/chevron.svg?svgr';

import styles from './ButtonRow.module.scss';

type ButtonRowProps = {
  icon?: ReactNode;
  title: string;
  description?: string;
  rightInfo?: string;
  color?: TextProps['color'];
  disabled?: boolean;
  chevron?: boolean;
} & DetailedHTMLProps<ButtonHTMLAttributes<HTMLButtonElement>, HTMLButtonElement>;

const ButtonRow: FC<ButtonRowProps> = ({
  icon,
  title,
  description,
  rightInfo,
  disabled,
  onClick,
  color = 'black',
  chevron = true,
}) => {
  return (
    <Button
      variant='ghost'
      color={color}
      className={styles.root}
      disabled={disabled}
      onClick={onClick}
      wFull
    >
      {!!icon && icon}
      <div className={styles.mainBlock}>
        <TextBox variant='16' align='left' color={color}>
          {title}
        </TextBox>
        {!!description && (
          <TextBox variant='10' color='greyOne' align='left'>
            {description}
          </TextBox>
        )}
      </div>
      <div className={styles.rightBlock}>
        {!!rightInfo && (
          <TextBox variant='16' color='greyOne'>
            {rightInfo}
          </TextBox>
        )}
        {chevron && <Chevron />}
      </div>
    </Button>
  );
};

export { ButtonRow };
