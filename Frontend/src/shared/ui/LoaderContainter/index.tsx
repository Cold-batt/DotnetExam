import { DetailedHTMLProps, FC, InputHTMLAttributes } from 'react';

import { Loader } from '../Loader';

import styles from './LoaderContainer.module.scss';

type LoaderContainerProps = {
  isLoading: boolean;
} & DetailedHTMLProps<InputHTMLAttributes<HTMLDivElement>, HTMLDivElement>;

const LoaderContainer: FC<LoaderContainerProps> = ({ isLoading, ...rest }) => {
  return (
    <>
      {isLoading ? (
        <div className={styles.root}>
          <Loader size='big' />
        </div>
      ) : (
        rest.children
      )}
    </>
  );
};

export { LoaderContainer };
