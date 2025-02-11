import { FC, useState } from "react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";

import { useRegistration } from "@/shared/api/services/auth/hooks/useRegistration";
import { ISignUpRequest } from "@/shared/api/services/auth/model";
import { PATH } from "@/shared/constants";
import { Button } from "@/shared/ui/Button";
import { Input } from "@/shared/ui/Input";
import { PasswordIndicator } from "@/shared/ui/PasswordIndicator";
import { TextBox } from "@/shared/ui/TextBox";

import ArrowIcon from "@/assets/icons/arrow.svg?svgr";
import DogIcon from "@/assets/icons/dog.svg?svgr";
import EyeCloseIcon from "@/assets/icons/eye.close.svg?svgr";
import EyeOpenIcon from "@/assets/icons/eye.open.svg?svgr";
import PasswordIcon from "@/assets/icons/password.svg?svgr";

import styles from "./SignUpPage.module.scss";
import { authUtils, validatePassword } from "@/shared/utils";

const SignUpPage: FC = () => {
  const navigate = useNavigate();
  const { handleSubmit, formState, watch, register } = useForm<ISignUpRequest>({
    mode: "onChange",
  });

  const { mutate: registration } = useRegistration({
    onSuccess: (response) => {
      authUtils.setToken(response.jwtToken);
      navigate(PATH.GAMES);
    },
  });

  const [passwordVisible, setPasswordVisible] = useState(false);
  const [confirmPasswordVisible, setConfirmPasswordVisible] = useState(false);

  const passwordPipe = watch("password");

  const onSubmit = (data: ISignUpRequest) => {
    registration(data);
  };

  const hanleChangePasswordVisabitlity = () => {
    setPasswordVisible(!passwordVisible);
  };

  const hanleChangeConfirmPasswordVisabitlity = () => {
    setConfirmPasswordVisible(!confirmPasswordVisible);
  };

  const handleNavigate = () => {
    navigate(PATH.SIGNIN);
  };

  const handleValidate = (value: string) => {
    return value === passwordPipe || "Passwords do not match";
  };

  return (
    <>
      <form onSubmit={handleSubmit(onSubmit)} className={styles.root}>
        <TextBox variant="32" fontWeight="500" align="center" color="black">
          Sign Up
        </TextBox>
        <div className={styles.fields}>
          <Input
            {...register("userName", {
              required: true,
            })}
            placeholder="Username"
            iconLeft={<DogIcon />}
            error={formState.errors?.userName?.message}
          />
          <div className={styles.gap8}>
            <Input
              {...register("password", {
                required: true,
                validate: validatePassword,
              })}
              type={passwordVisible ? "text" : "password"}
              placeholder="Password"
              iconLeft={<PasswordIcon />}
              iconRight={
                passwordVisible ? (
                  <EyeOpenIcon onClick={hanleChangePasswordVisabitlity} />
                ) : (
                  <EyeCloseIcon onClick={hanleChangePasswordVisabitlity} />
                )
              }
            />
            <PasswordIndicator password={passwordPipe} />
          </div>
          <Input
            {...register("confirmPassword", {
              required: "Password is required",
              validate: handleValidate,
            })}
            type={confirmPasswordVisible ? "text" : "password"}
            placeholder="Confirm password"
            iconLeft={<PasswordIcon />}
            iconRight={
              confirmPasswordVisible ? (
                <EyeOpenIcon onClick={hanleChangeConfirmPasswordVisabitlity} />
              ) : (
                <EyeCloseIcon onClick={hanleChangeConfirmPasswordVisabitlity} />
              )
            }
            error={formState.errors.confirmPassword?.message}
          />
        </div>
        <Button
          type="submit"
          variant="primary"
          disabled={!formState.isValid}
          color="white"
        >
          Sign Up
        </Button>
        <div className={styles.link}>
          <TextBox variant="14" align="center" color="greyOne">
            Already have an account?{" "}
          </TextBox>
          <TextBox
            variant="14"
            as="button"
            type="button"
            color="black"
            onClick={handleNavigate}
          >
            Log in
          </TextBox>

          <ArrowIcon />
        </div>
      </form>
    </>
  );
};

export default SignUpPage;
