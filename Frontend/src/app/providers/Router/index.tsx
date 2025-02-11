import { FC } from 'react';
import { RouterProvider, createBrowserRouter } from 'react-router-dom';

import { routes } from './routes';

const router = createBrowserRouter(routes);

const Router: FC = () => {
  return <RouterProvider router={router} />;
};

export default Router;
