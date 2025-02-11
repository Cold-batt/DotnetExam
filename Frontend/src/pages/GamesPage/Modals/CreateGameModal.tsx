import { FC } from "react";
import { useForm } from "react-hook-form";

import { Button } from "@/shared/ui/Button";
import { Input } from "@/shared/ui/Input";
import { ModalWindow } from "@/shared/ui/ModalWindow";

import styles from "./Modals.module.scss";
import { BaseModalProps } from "@/shared/types";
import { useCreateGame } from "@/shared/api/services/games/hooks/useCreateGame";
import { ICreateGameRequest } from "@/shared/api/services/games/module";

const CreateGameModal: FC<BaseModalProps> = ({ open, setOpen }) => {
  const { mutate, isPending } = useCreateGame({
    onSuccess: () => {
      handleClose();
    },
  });

  const { register, handleSubmit, formState, reset } =
    useForm<ICreateGameRequest>({
      mode: "onSubmit",
    });

  const handleClose = () => {
    reset();
    setOpen(false);
  };

  const onSubmit = (data: ICreateGameRequest) => {
    mutate(data);
  };

  return (
    <>
      <ModalWindow title="Create Game" open={open} setOpen={handleClose}>
        <form className={styles.root}>
          <Input
            {...register("maxRate", {
              required: true,
              validate: (val) => {
                return val > 0 && val < 1000000;
              },
            })}
            type="number"
            error={formState.errors.maxRate?.message}
            placeholder="Max Rate"
          />
          <Button
            onClick={handleSubmit(onSubmit)}
            variant="primary"
            disabled={!formState.isValid}
            isLoading={isPending}
            color="white"
            wFull
          >
            Submit
          </Button>
        </form>
      </ModalWindow>
    </>
  );
};

export { CreateGameModal };
