import { FC } from "react";

import styles from "./RoomPage.module.scss";
import { Input } from "@/shared/ui/Input";
import { useForm } from "react-hook-form";
import { Button } from "@/shared/ui/Button";

import CrossIcon from "@/assets/icons/cross.svg?svgr";
import CircleIcon from "@/assets/icons/circle.svg?svgr";
import { TextBox } from "@/shared/ui/TextBox";

const field = [0, 0, 1, 0, 0, 0, -1, 0, 0];

const fillBoard = (board: number[]) => {
  return board.map((el) => {
    switch (el) {
      case 1:
        return (
          <div className={styles.boardCell}>
            <CrossIcon />
          </div>
        );
      case -1:
        return (
          <div className={styles.boardCell}>
            <CircleIcon />
          </div>
        );
      default:
        return <div className={styles.boardCell}></div>;
    }
  });
};

const RoomPage: FC = () => {
  const {
    register,
    formState: { isValid },
  } = useForm<{ message: string }>();

  return (
    <form className={styles.root}>
      <div className={styles.gameBoard}>{fillBoard(field)}</div>
      <div className={styles.chatBlock}>
        <div className={styles.scroll}>
          {field.map((item) => (
            <TextBox variant="60">{item}</TextBox>
          ))}
        </div>

        <div className={styles.send}>
          <Input
            placeholder="Type your message"
            {...register("message", { required: true })}
            className={styles.customInput}
          />
          <Button disabled={!isValid} size="small">
            Send
          </Button>
        </div>
      </div>
    </form>
  );
};

export default RoomPage;
