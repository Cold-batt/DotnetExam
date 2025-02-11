import { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";
import { authUtils } from "@/shared/utils";

export const useGameHub = (gameId: string) => {
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
  const [board, setBoard] = useState<number[]>(Array(9).fill(0)); // Игровое поле
  const [currentTurn, setCurrentTurn] = useState<number>(1); // 1 - крестики, -1 - нолики
  const [playerSymbol, setPlayerSymbol] = useState<number | null>(null); // Кто играет за текущего пользователя

  const token =authUtils.getToken()

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:9000/hub', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        accessTokenFactory: () => { return token ?? ''}
      })
      .withAutomaticReconnect()
      .build();

    newConnection
      .start()
      .then(async () => {
        console.log("SignalR Connected", gameId);
        await newConnection.invoke("Join", gameId);
      })
      .catch(err => console.error("SignalR Connection Error: ", err));

    // Получаем обновленное состояние игры
    newConnection.on("GameUpdated", (newBoard: number[], turn: number) => {
      setBoard(newBoard);
      setCurrentTurn(turn);
    });

    // Узнаем, за кого играет пользователь
    newConnection.on("PlayerAssigned", (symbol: number) => {
      setPlayerSymbol(symbol);
    });

    setConnection(newConnection);

    return () => {
      newConnection.stop();
    };
  }, [gameId]);

  const makeMove = async (index: number) => {
    if (!connection || board[index] !== 0 || playerSymbol !== currentTurn) return;
    try {
      await fetch(`/Game/move?index=${index}`, { method: "POST" });
    } catch (error) {
      console.error("Move error:", error);
    }
  };

  return { connection, board, makeMove, currentTurn, playerSymbol };
};
