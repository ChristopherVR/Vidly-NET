export interface User {
  id: number;
  name: string;
  surname: string;
  userName: string;
}

export interface RegisterUser {
  name: string;
  surname: string;
  userName: string;
  password: string;
}
