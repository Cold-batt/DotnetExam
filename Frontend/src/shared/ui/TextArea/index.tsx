import clsx from 'clsx';
import { DetailedHTMLProps, FC, TextareaHTMLAttributes, forwardRef } from 'react';

import { TextBox } from '../TextBox';

import styles from './TextArea.module.scss';

type TextAreaType = {
  hasError?: boolean;
} & DetailedHTMLProps<TextareaHTMLAttributes<HTMLTextAreaElement>, HTMLTextAreaElement>;

const TextArea: FC<TextAreaType> = forwardRef(({ hasError, maxLength = 2000, ...rest }, ref) => {
  return (
    <div className={styles.root}>
      <textarea
        ref={ref}
        className={clsx(styles.textArea, {
          [styles.error]: hasError,
        })}
        {...rest}
      />
      {!!maxLength && (
        <div className={styles.description}>
          <TextBox variant='12' color={hasError ? 'red' : 'black'}>
            {(rest.value ?? '')?.toString().length}
            <TextBox as='span' color='greyOne'>
              {` / ${maxLength}`}
            </TextBox>
          </TextBox>
        </div>
      )}
    </div>
  );
});

export { TextArea };
