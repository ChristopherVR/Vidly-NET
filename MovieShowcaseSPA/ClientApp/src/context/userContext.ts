import { createContext } from 'react';
import { User } from '../interfaces/user';

type UserProps = {
  user?: User;
  setUser?: React.Dispatch<React.SetStateAction<User | undefined>>;
};

const initialUserContext: UserProps = {
  user: undefined,
  setUser: undefined,
};

const UserContext = createContext(initialUserContext);

UserContext.displayName = 'UserContext';

export default UserContext;
