import { FC } from "react";

import QueryProvider from "./providers/QueryProvider";
import Router from "./providers/Router";

import "@/shared/styles/color.scss";
import "@/shared/styles/index.scss";

const App: FC = () => {
  return (
    <QueryProvider>
      <Router />
    </QueryProvider>
  );
};

export { App };
