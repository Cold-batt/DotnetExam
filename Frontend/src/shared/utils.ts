import { useEffect, useRef, useState } from "react";

export const validatePassword = (value: string) => {
  return value?.length >= 8 && /[A-Z]/.test(value) && /[a-z]/.test(value) && /[0-9]/.test(value);
};

export const authUtils = {
  getToken() {
    return localStorage.getItem('tic-tac-toe-token');
  },
  setToken(token: string) {
    localStorage.setItem('tic-tac-toe-token', token);
  },
  deleteToken() {
    localStorage.removeItem('tic-tac-toe-token');
  },
};

export const useOnScreen = <T extends HTMLElement>(rootMargin = '0px') => {
  const [isVisible, setIsVisible] = useState(false);
  const containerRef = useRef<T>(null);

  useEffect(() => {
    const observer = new IntersectionObserver(
      ([entry]) => {
        setIsVisible(entry.isIntersecting);
      },
      {
        rootMargin,
      },
    );

    const currentElement = containerRef?.current;

    if (currentElement) {
      observer.observe(currentElement);

      return () => {
        observer.unobserve(currentElement);
      };
    }
  }, [rootMargin]);

  return [containerRef, isVisible] as const;
};
