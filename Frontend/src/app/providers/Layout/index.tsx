import { FC, Suspense, useEffect } from "react";
import { Outlet, useLocation, useNavigate } from "react-router-dom";

import { Loader } from "@/shared/ui/Loader";
import { authUtils } from "@/shared/utils";
import { PATH } from "@/shared/constants";

const Layout: FC = () => {
  const location = useLocation();
  const navigate = useNavigate();

  useEffect(() => {
    const token = authUtils.getToken();
    if (
      !token &&
      location.pathname !== PATH.SIGNIN &&
      location.pathname !== PATH.SIGNUP
    ) {
      navigate(PATH.SIGNIN);
      return;
    }
    if (location.pathname === "/") {
      navigate(PATH.GAMES);
    }
  }, [navigate, location]);

  return (
    <Suspense fallback={<Loader size="big" />}>
      <Outlet />
      {/* {showNavBar && <NavBar />} */}
    </Suspense>
  );
};

export default Layout;
