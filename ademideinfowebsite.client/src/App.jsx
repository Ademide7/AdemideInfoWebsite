import { CssBaseline, ThemeProvider } from "@mui/material";
import { BrowserRouter } from "react-router-dom";

import AppRoutes from "./routes/AppRoutes";
import AuthProvider from "./context/AuthProvider";
import theme from "./theme/theme";

function App() {
    return (
        <ThemeProvider theme={theme}>
            <CssBaseline />

            <BrowserRouter>
                <AuthProvider>
                    <AppRoutes />
                </AuthProvider>
            </BrowserRouter>
        </ThemeProvider>
    );
}

export default App;