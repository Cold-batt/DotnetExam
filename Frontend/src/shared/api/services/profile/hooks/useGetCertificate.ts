import { useMutation } from '@tanstack/react-query';

import { IMutationOptions } from '@/shared/api/model';

import { ProfileService } from '..';

export const KEY_CERT = 'users/me/certificate';

export const useGetCertificate = (options?: IMutationOptions<string>) =>
  useMutation({
    mutationKey: [KEY_CERT],
    mutationFn: () => ProfileService.getCertificate(),
    ...options,
  });
