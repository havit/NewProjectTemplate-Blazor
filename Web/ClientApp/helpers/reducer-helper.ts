// TODO: should be npm package

export type Action = {
    type: string;
}

export type Omit<T, K extends keyof T> = Pick<T, Exclude<keyof T, K>>;  // TODO: should be placed somewhere else

const enumToArray = (actionTypesEnum: Object): string[] => Object.keys(actionTypesEnum).map(key => actionTypesEnum[key]);

const isKnownAction = <A extends Action>(actionTypes: string[], action: A): boolean => actionTypes.some(actionType => actionType === action.type);

const createNewStateByMerge = <S>(state, { type, ...actionData }: Action): S => ({ ...state, ...actionData });

export const createGenericReducer = <S, A extends Action>(relevantActionTypesEnum: Object, initialState: S) => {
    const actionTypes = enumToArray(relevantActionTypesEnum);

    return (state: S = initialState, action: A): S => isKnownAction<A>(actionTypes, action)
        ? createNewStateByMerge<S>(state, action)
        : state;
}