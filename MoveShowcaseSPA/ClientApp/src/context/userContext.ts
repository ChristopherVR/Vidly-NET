import { createContext } from 'react';
import { User } from '../interfaces/user';

const UserContext = createContext({
  user: {} as User,
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  setUser: (user: User) => {},
});
UserContext.displayName = 'UserContext';

export default UserContext;
