import clsx from "clsx";
import {
  DetailedHTMLProps,
  FocusEvent,
  InputHTMLAttributes,
  ReactNode,
  forwardRef,
  useImperativeHandle,
  useRef,
} from "react";

import { TextBox } from "../TextBox";

import styles from "./Input.module.scss";

export type InputType = {
  label?: string;
  mask?: string;
  iconLeft?: ReactNode;
  iconRight?: ReactNode;
  error?: string;
  required?: boolean;
  variant?: "primary" | "ghost";
} & DetailedHTMLProps<InputHTMLAttributes<HTMLInputElement>, HTMLInputElement>;

export const Input = forwardRef<HTMLInputElement, InputType>(
  (
    {
      iconLeft,
      iconRight,
      className,
      placeholder = "",
      label,
      disabled,
      error,
      required,
      // variant = "primary",
      ...rest
    },
    ref
  ) => {
    const wrapperRef = useRef<HTMLDivElement>(null);
    const initialRef = useRef<HTMLInputElement>(null);

    useImperativeHandle(ref, () => initialRef.current!);

    const handleFocus = (e: FocusEvent<HTMLInputElement>) => {
      rest?.onFocus?.(e);

      wrapperRef.current?.classList.add("activeInput");
    };

    const handleBlur = (e: FocusEvent<HTMLInputElement>) => {
      rest?.onBlur?.(e);

      wrapperRef.current?.classList.remove("activeInput");
    };

    return (
      <div className={styles.root}>
        <div className={clsx(className, styles.wrapper)}>
          {iconLeft && <div className={styles.icon}>{iconLeft}</div>}

          <div
            ref={wrapperRef}
            className={clsx(styles.wrapper_block, {
              [styles.error]: !!error,
              [styles.disabled]: !!disabled,
            })}
          >
            <input
              className={clsx(styles.floatingInput, {
                [styles.placeholderOff]: label && !placeholder,
              })}
              ref={initialRef}
              disabled={disabled}
              placeholder={required ? `${placeholder} *` : placeholder}
              {...rest}
              onFocus={handleFocus}
              onBlur={handleBlur}
            />
            {label && (
              <label
                className={clsx(styles.label, {
                  [styles.labelActive]: label && placeholder,
                })}
              >
                {required ? `${label} *` : label}
              </label>
            )}
            {iconRight && (
              <div className={clsx(styles.icon, styles.iconRight)}>
                {iconRight}
              </div>
            )}
          </div>
        </div>
        {error && (
          <TextBox
            variant="10"
            color="red"
            className={clsx(iconLeft ? styles.errorMessage : "")}
          >
            {error}
          </TextBox>
        )}
      </div>
    );
  }
);

Input.displayName = "Input";
