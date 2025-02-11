import { useQuery } from '@tanstack/react-query';

import { IQueryOptions } from '@/shared/api/model';

import { ReferencesService } from '..';
import { ITagsRequest } from '../model';

export const KEY_TAGS = 'references/tags';

export const useTags = (params: ITagsRequest, options?: IQueryOptions<string[]>) =>
  useQuery({
    queryKey: [KEY_TAGS, params.tag],
    queryFn: () => ReferencesService.getTags(params),
    ...options,
  });
