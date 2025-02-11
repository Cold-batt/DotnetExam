import BaseDropdownMenu from './DropdownMenu';
import DropdownMenuItem from './DropdownMenuItem';

type ExtendedDropdownMenu = typeof BaseDropdownMenu & {
  Item: typeof DropdownMenuItem;
};

const DropdownMenu = BaseDropdownMenu as ExtendedDropdownMenu;
DropdownMenu.Item = DropdownMenuItem;

export default DropdownMenu;
export type { DropdownMenuProps } from './DropdownMenu';
export type { DropdownMenuItemProps } from './DropdownMenuItem';
