export interface BaseModalProps {
  open: boolean;
  setOpen: (value: boolean) => void;
}

export interface IBaseError {
  detail?: IBaseErrorItem[];
}

export interface IBaseErrorItem {
  input: string;
  msg: string;
  type: string;
}