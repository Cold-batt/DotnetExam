import clsx from 'clsx';
import { ChangeEvent, FC, useRef, useState } from 'react';
import { Swiper, SwiperClass, SwiperSlide } from 'swiper/react';
import 'swiper/scss';
import 'swiper/scss/navigation';
import 'swiper/scss/pagination';
import 'swiper/scss/scrollbar';

import { Button } from '../Button';
import { MultiViewBox } from '../MultiViewBox';
import { TextBox } from '../TextBox';

import CameraIcon from '@/assets/icons/camera.svg?svgr';
import Chevron from '@/assets/icons/chevron.svg?svgr';
import XMarkIcon from '@/assets/icons/xmark.svg?svgr';

import styles from './UploadFile.module.scss';

type UploadFileProps = {
  value: string[];
  onChange: (newVal: string[]) => void;
};

const UploadFile: FC<UploadFileProps> = ({ value, onChange }) => {
  const ref = useRef<HTMLInputElement>(null);
  const [swiper, setSwiper] = useState<SwiperClass>();
  const [activeIndex, setActiveIndex] = useState<number>(0);
  const [error, setError] = useState<string | undefined>();

  const uploadHandler = async (e: ChangeEvent<HTMLInputElement>) => {
    setError(undefined);
    if (e.target.files === null) {
      return;
    }

    const file = e.target.files[0];
    if (file) {
      const fileReader = new FileReader();

      const maxSizeInBytes = 100 * 1024 * 1024;

      if (file.size > maxSizeInBytes) {
        setError('The file must be no more than 100 MB in size.');
        return;
      }

      fileReader.onload = (event) => {
        const fileContent = event.target?.result;

        if (typeof fileContent === 'string') {
          const uploadedFiles = [...(value ?? []), fileContent];
          onChange(uploadedFiles);
          setTimeout(() => {
            swiper?.slideTo(uploadedFiles.length - 1);
          }, 0);
        }
      };

      fileReader.readAsDataURL(file);
    }
  };

  return (
    <div className={styles.root}>
      <input
        ref={ref}
        type='file'
        id='picture'
        accept='.jpg, .png, .gif, .svg, .mp4, .webm, .mp3, .wav, .ogg, .glb, .gltf'
        className={styles.displayNone}
        onChange={(event) => uploadHandler(event)}
        value=''
      />

      {!value?.length ? (
        <div className={styles.emptyView} onClick={() => ref.current?.click()}>
          <div className={styles.button}>
            <Button variant='secondary' size='small'>
              <CameraIcon />
              <TextBox variant='14'>Upload</TextBox>
            </Button>
          </div>

          <TextBox variant='10' color='greyOne' className={styles.description}>
            jpg, png, gif, svg, mp4, webm, mp3, wav, ogg, glb, gltf... The maximum size is 100 Mb.
            You can upload several works at once!Make sure`` to upload a high-quality image here. In
            case you choose NFT minting or IP rights protection services, this metadata will be
            added to the blockchain.
          </TextBox>
        </div>
      ) : (
        <div className={styles.imageView}>
          <Swiper
            className={styles.swiper}
            slidesPerView={1}
            onSlideChange={(swiper) => setActiveIndex(swiper.activeIndex)}
            onSwiper={(swiper) => {
              setSwiper(swiper);
            }}
          >
            {value.map((file, i) => (
              <SwiperSlide key={i} className={styles.slide}>
                <MultiViewBox content={file} />
              </SwiperSlide>
            ))}
          </Swiper>

          <div className={styles.uploadMoreButton}>
            <Button
              variant='secondary'
              size='small'
              onClick={() => {
                ref.current?.click();
              }}
            >
              <CameraIcon />
              <TextBox variant='14'>Upload more</TextBox>
            </Button>
          </div>

          <div className={styles.deleteButton}>
            <Button
              size='small'
              variant='secondary'
              onClick={() => {
                onChange(value.filter((_, i) => i !== activeIndex));
                swiper?.slideToClosest();
              }}
            >
              <XMarkIcon />
            </Button>
          </div>
        </div>
      )}
      <div className={styles.navigation}>
        <Chevron
          className={clsx(styles.chevron, styles.rotate, {
            [styles.active]: !!value?.length && activeIndex > 0,
          })}
          onClick={() => {
            swiper?.slidePrev();
          }}
        />

        <div>
          <TextBox as='span' color='black' variant='12'>
            {!!value?.length ? activeIndex + 1 : 0} /{' '}
            <TextBox as='span' color='greyOne' variant='12'>
              {value?.length ?? 0}
            </TextBox>
          </TextBox>
        </div>

        <Chevron
          className={clsx(styles.chevron, {
            [styles.active]: !!value?.length && activeIndex + 1 < value?.length,
          })}
          onClick={() => swiper?.slideNext()}
        />
      </div>
      {!!error && <TextBox color='red'>{error}</TextBox>}
    </div>
  );
};

export { UploadFile };
