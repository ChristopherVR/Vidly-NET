export interface Column {
  path: string;
  label: string;
  content: (content: unknown) => JSX.Element;
  key?: unknown;
}

export interface SortColumn {
  order: boolean | 'asc' | 'desc';
  path: string;
}
