import { FC, useState } from "react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";

import { useLogin } from "@/shared/api/services/auth/hooks/useLogin";
import { ISignInRequest } from "@/shared/api/services/auth/model";
import { PATH } from "@/shared/constants";
import { Button } from "@/shared/ui/Button";
import { Input } from "@/shared/ui/Input";
import { TextBox } from "@/shared/ui/TextBox";

import ArrowIcon from "@/assets/icons/arrow.svg?svgr";
import DogIcon from "@/assets/icons/dog.svg?svgr";
import EyeCloseIcon from "@/assets/icons/eye.close.svg?svgr";
import EyeOpenIcon from "@/assets/icons/eye.open.svg?svgr";
import PasswordIcon from "@/assets/icons/password.svg?svgr";

import styles from "./SignInPage.module.scss";
import { authUtils } from "@/shared/utils";

const SignInPage: FC = () => {
  const navigate = useNavigate();
  const { handleSubmit, formState, register, setError } =
    useForm<ISignInRequest>({
      mode: "onChange",
    });

  const { mutate: login, isPending } = useLogin({
    onSuccess: (response) => {
      authUtils.setToken(response.jwtToken);
      navigate(PATH.GAMES);
    },
    onError: () => {
      setError("userName", { message: "Invalid username or password." });
    },
  });

  const [passwordVisible, setPasswordVisible] = useState(false);

  const onSubmit = (data: ISignInRequest) => {
    login(data);
  };

  const handleChangeVisability = () => {
    setPasswordVisible(!passwordVisible);
  };

  const handleNavigate = () => {
    navigate(PATH.SIGNUP);
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className={styles.root}>
      <TextBox variant="32" fontWeight="500" align="center" color="black">
        Sign In
      </TextBox>
      <div className={styles.fields}>
        <Input
          {...register("userName", {
            required: true,
          })}
          placeholder="Username"
          iconLeft={<DogIcon />}
          error={formState.errors.userName?.message}
        />
        <Input
          {...register("password", {
            required: true,
          })}
          type={passwordVisible ? "text" : "password"}
          placeholder="Password"
          iconLeft={<PasswordIcon />}
          iconRight={
            passwordVisible ? (
              <EyeOpenIcon onClick={handleChangeVisability} />
            ) : (
              <EyeCloseIcon onClick={handleChangeVisability} />
            )
          }
        />
      </div>
      <Button
        type="submit"
        variant="primary"
        disabled={!formState.isValid}
        color="white"
        isLoading={isPending}
      >
        Sign In
      </Button>
      <div className={styles.link}>
        <TextBox variant="14" align="center" color="greyOne">
          Don't have an account?{" "}
        </TextBox>
        <TextBox
          variant="14"
          as="button"
          type="button"
          color="black"
          onClick={handleNavigate}
        >
          Sign up
        </TextBox>

        <ArrowIcon />
      </div>
    </form>
  );
};

export default SignInPage;
