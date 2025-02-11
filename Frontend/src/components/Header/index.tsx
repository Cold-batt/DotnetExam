import { FC, useState } from "react";
import styles from "./Header.module.scss";
import { TextBox } from "@/shared/ui/TextBox";
import { Button } from "@/shared/ui/Button";
import { CreateGameModal } from "@/pages/GamesPage/Modals/CreateGameModal";

const Header: FC = () => {
  const [showCreateModal, setShowCreateModal] = useState(false);

  return (
    <div className={styles.root}>
      <TextBox color="white" variant="18">
        TicTacToe
      </TextBox>
      <div className={styles.buttons}>
        <Button variant="secondary" size="small">
          Rating
        </Button>
        <Button
          variant="secondary"
          size="small"
          onClick={() => setShowCreateModal(true)}
        >
          + Create New
        </Button>
      </div>
      <CreateGameModal open={showCreateModal} setOpen={setShowCreateModal} />
    </div>
  );
};

export { Header };
