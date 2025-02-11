import { FC } from 'react';

import { TextBox } from '../TextBox';

import styles from './Avatar.module.scss';

type AvatarType = {
  src?: string;
  bgColor?: string;
  letter?: string;
  size?: '112' | '158';
  onClick?: () => void;
};

const Avatar: FC<AvatarType> = ({ src, bgColor, letter, onClick, size = 112 }) => {
  return (
    <div
      className={styles.root}
      style={{ backgroundColor: bgColor, width: `${size}px`, height: `${size}px` }}
      onClick={() => onClick?.()}
    >
      {src ? (
        <img src={src} alt='avatar icon' />
      ) : (
        <TextBox variant='32' color='white'>
          {letter}
        </TextBox>
      )}
    </div>
  );
};

export { Avatar };
