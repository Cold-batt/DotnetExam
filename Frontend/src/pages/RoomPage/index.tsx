import { FC, useState } from "react";
import { useGameHub } from "@/hooks/useGameHub";
import { Input } from "@/shared/ui/Input";
import { useForm } from "react-hook-form";
import { Button } from "@/shared/ui/Button";
import CrossIcon from "@/assets/icons/cross.svg?svgr";
import CircleIcon from "@/assets/icons/circle.svg?svgr";
import { TextBox } from "@/shared/ui/TextBox";
import styles from "./RoomPage.module.scss";
import { useJoinGame } from "@/shared/api/services/games/hooks/useJoinGame";
import { useParams } from "react-router-dom";

const RoomPage: FC = () => {
  const { id } = useParams();

  const [gameId, setGameId] = useState<string | null>(null);
  const { board, makeMove, currentTurn, playerSymbol } = useGameHub(id || "");
  const {
    register,
    formState: { isValid },
  } = useForm<{ message: string }>();

  const { mutate: join } = useJoinGame({
    onSuccess: (response) => {
      setGameId(response.gameId);
    },
  });

  return (
    <div className={styles.root}>
      {!gameId && !!id ? (
        <Button onClick={() => join({ gameId: id })}>Join Game</Button>
      ) : (
        <>
          <h3>
            {playerSymbol === currentTurn
              ? "Your step"
              : "Waiting for oponents"}
          </h3>
          <div className={styles.gameBoard}>
            {board.map((el, index) => (
              <div
                key={index}
                className={`${styles.boardCell} ${
                  el !== 0 ? styles.disabled : ""
                }`}
                onClick={() =>
                  el === 0 && playerSymbol === currentTurn && makeMove(index)
                }
              >
                {el === 1 ? <CrossIcon /> : el === -1 ? <CircleIcon /> : null}
              </div>
            ))}
          </div>
          <div className={styles.chatBlock}>
            <div className={styles.scroll}>
              {board.map((item, index) => (
                <TextBox key={index} variant="60">
                  {item}
                </TextBox>
              ))}
            </div>
            <div className={styles.send}>
              <Input
                placeholder="Type your message"
                {...register("message", { required: true })}
              />
              <Button disabled={!isValid} size="small">
                Send
              </Button>
            </div>
          </div>
        </>
      )}
    </div>
  );
};

export default RoomPage;
