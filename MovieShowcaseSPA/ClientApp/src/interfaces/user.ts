export interface User {
  id: number;
  name: string;
  surname: string;
  userName: string;
  imageUrl?: string;
}

export interface UpdateUser {
  id: number;
  name: string;
  surname: string;
  homeNumber: string;
  personalNumber: string;
  address: string;
  imageUrl?: string;
}

export interface RegisterUser {
  name: string;
  surname: string;
  userName: string;
  password: string;
  homeNumber: string;
  phoneNumber: string;
  address: string;
  imageUrl?: string;
}
