import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';

import { TwinService } from '..';
import { IUploadTwinItemRequest } from '../model';

export const KEY_UPLOAD_TWIN_IMAGE = 'uploadTwinImage';

export const useUploadTwinImage = (options?: IMutationOptions<void, IUploadTwinItemRequest>) =>
  useMutation({
    mutationKey: [KEY_UPLOAD_TWIN_IMAGE],
    mutationFn: (data) => TwinService.uploadTwinImage(data),
    ...options,
  });
