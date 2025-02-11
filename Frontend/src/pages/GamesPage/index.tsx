import { Header } from "@/components/Header";
import { useGetGames } from "@/shared/api/services/games/hooks/useGetGames";
import { LoaderContainer } from "@/shared/ui/LoaderContainter";
import LoadMore from "@/shared/ui/LoadMore";
import { FC } from "react";
import styles from "./GamesPage.module.scss";
import { TextBox } from "@/shared/ui/TextBox";
import { GameCard } from "@/components/GameCard";

const GamesPage: FC = () => {
  const {
    data: gamesResponse,
    isLoading,
    isFetching,
    hasNextPage,
    fetchNextPage,
    refetch,
  } = useGetGames({});

  const games = gamesResponse?.pages?.flatMap((el) => el.entities);

  return (
    <div className={styles.root}>
      <Header refetch={refetch} />

      <LoaderContainer isLoading={isLoading}>
        {games && (
          <div className={styles.games}>
            <TextBox variant="32">Active Games</TextBox>
            <div>
              {games.map((game) => (
                <GameCard
                  gameId={game.gameId}
                  gameState={game.gameState}
                  maxRate={game.maxRate}
                />
              ))}
              {!isFetching && hasNextPage && (
                <LoadMore onVisible={fetchNextPage} />
              )}
            </div>
          </div>
        )}
      </LoaderContainer>
    </div>
  );
};

export default GamesPage;
