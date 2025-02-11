import { lazy } from "react";
import { RouteObject } from "react-router-dom";

import Layout from "@/app/providers/Layout";
import { PATH } from "@/shared/constants";

const SignInPage = lazy(() => import("@/pages/SignInPage"));
const SignUpPage = lazy(() => import("@/pages/SignUpPage"));

const GamesPage = lazy(() => import("@/pages/GamesPage"));
const RoomPage = lazy(() => import("@/pages/RoomPage"));

export const routes: RouteObject[] = [
  {
    path: PATH.MAIN,
    element: <Layout />,
    children: [
      {
        path: PATH.SIGNIN,
        element: <SignInPage />,
      },
      {
        path: PATH.SIGNUP,
        element: <SignUpPage />,
      },
      {
        path: PATH.GAMES,
        element: <GamesPage />,
      },
      {
        path: `${PATH.ROOM}/:id`,
        element: <RoomPage />,
      },
    ],
  },
];
