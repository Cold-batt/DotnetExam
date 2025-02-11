import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';

import { TwinService } from '..';
import { IUploadTwinItemRequest } from '../model';

export const KEY_UPLOAD_TWIN_VIDEO = 'uploadTwinVideo';

export const useUploadTwinVideo = (options?: IMutationOptions<void, IUploadTwinItemRequest>) =>
  useMutation({
    mutationKey: [KEY_UPLOAD_TWIN_VIDEO],
    mutationFn: (data) => TwinService.uploadTwinVideo(data),
    ...options,
  });
