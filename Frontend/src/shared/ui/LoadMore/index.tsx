import { useOnScreen } from "@/shared/utils";
import { FC, useEffect } from "react";

interface LoadMoreProps {
  className?: string;
  onVisible: () => void;
}

const LoadMore: FC<LoadMoreProps> = ({ className, onVisible }) => {
  const [listEndRef, isVisible] = useOnScreen<HTMLDivElement>();

  useEffect(() => {
    if (isVisible) onVisible();
  }, [isVisible, onVisible]);

  return <div ref={listEndRef} className={className} style={{ height: 2 }} />;
};

export default LoadMore;
