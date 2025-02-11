import { IGame } from "@/shared/api/services/games/module";
import { Button } from "@/shared/ui/Button";
import { TextBox } from "@/shared/ui/TextBox";
import { FC } from "react";

import styles from "./GameCard.module.scss";
import { useNavigate } from "react-router-dom";
import { PATH } from "@/shared/constants";
import { Separator } from "@/shared/ui/Separator";

const getState = (state: number) => {
  switch (state) {
    case 0:
      return "Created";
    case 1:
      return "Started";
    default:
      return "Finished";
  }
};

const GameCard: FC<IGame> = ({ gameId, gameState, maxRate }) => {
  const navigate = useNavigate();

  const handleOpen = () => {
    navigate(`${PATH.ROOM}/${gameId}`);
  };

  return (
    <div className={styles.root}>
      <TextBox variant="24">Game №{gameId}</TextBox>
      <Separator />
      <div className={styles.info}>
        <TextBox variant="16">State: {getState(gameState)}</TextBox>
        <TextBox variant="16">Max Rate: {maxRate}</TextBox>
      </div>
      <Button size="small" onClick={handleOpen}>
        Open
      </Button>
    </div>
  );
};

export { GameCard };
