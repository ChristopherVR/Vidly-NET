import { createContext } from 'react';
import { User } from '../interfaces/user';

const UserContext = createContext({} as User);
UserContext.displayName = 'UserContext';

export default UserContext;
