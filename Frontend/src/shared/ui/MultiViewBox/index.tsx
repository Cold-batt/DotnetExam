import { FC } from 'react';

import { TextBox } from '../TextBox';

import styles from './MultiViewBox.module.scss';

interface MultiViewBoxType {
  content: string;
}

const MultiViewBox: FC<MultiViewBoxType> = ({ content }) => {
  switch (content.split('/').at(0)) {
    case 'data:image':
      return <img src={content} alt='Uploaded' />;
    case 'data:video':
      return (
        <video controls>
          <source src={content} type={content.match(/:(.*?);/)?.[1] || ''} />
          Your browser does not support the video tag.
        </video>
      );
    case 'data:audio':
      return (
        <audio controls style={{ width: '100%' }}>
          <source src={content} type={content.match(/:(.*?);/)?.[1] || ''} />
          Your browser does not support the audio element.
        </audio>
      );
    default:
      return (
        <div className={styles.unsupported}>
          <TextBox variant='16'>Unsupported file type</TextBox>
        </div>
      );
  }
};

export { MultiViewBox };
