/// <reference types="vite/client" />

declare module '*.svg?svgr' {
  const content: React.FC<React.SVGProps<SVGElement>>;
  export default content;
}