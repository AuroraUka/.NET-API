import {  PropsWithChildren, createContext, useContext, useState } from "react";

interface PhoneBookContextValue {
    shouldRefetch: boolean;
    setShouldRefetch: (shouldRefetch : boolean) => void;
}

export const PhoneBookContext = createContext<PhoneBookContextValue | undefined>(undefined);



export const usePhoneBookContext = () => {
    const context = useContext(PhoneBookContext);
    if (!context) {
        throw  Error('usePhoneBookContext must be used within a PhoneBookProvider');
    }
    return context;
};

export const PhoneBookProvider = ({ children }: PropsWithChildren<unknown>) => {
    const [shouldRefetch, setShouldRefetch] = useState(true);

    return (
        <PhoneBookContext.Provider value={{ shouldRefetch, setShouldRefetch }}>
            {children}
        </PhoneBookContext.Provider>
    );
};