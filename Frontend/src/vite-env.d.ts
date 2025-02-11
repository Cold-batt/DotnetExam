/// <reference types="vite/client" />

declare module '*.svg?svgr' {
  const content: React.FC<React.SVGProps<SVGElement>>;
  export default content;
}

type PolymorphicProps<T extends ElementType, E extends object = EmtyObject> = { as?: T } & E &
  Omit<React.ComponentPropsWithoutRef<T>, keyof E | 'as'>;

type Nullable<T> = T | null;

type ValueOf<T> = T[keyof T];

type EnumValues<T> = `${T}`;

type Numeric = `${number}` | number;

type EmptyString = '';

type ClassNameMap<ClassKey extends string = string> = Record<ClassKey, string>;

type Classes<T extends string> = Partial<ClassNameMap<T>>;