import { createTheme } from "@mui/material/styles";

const theme = createTheme({
    palette: {
        mode: "light",

        primary: {
            main: "#2563EB", // Modern Blue
            light: "#60A5FA",
            dark: "#1E40AF",
            contrastText: "#fff",
        },

        secondary: {
            main: "#7C3AED", // Purple Accent
        },

        success: {
            main: "#16A34A",
        },

        warning: {
            main: "#F59E0B",
        },

        error: {
            main: "#DC2626",
        },

        text: {
            primary: "#111827",
            secondary: "#6B7280",
        },

        background: {
            default: "#F8FAFC",
            paper: "#FFFFFF",
        },

        divider: "#E5E7EB",
    },

    shape: {
        borderRadius: 18,
    },

    typography: {
        fontFamily: [
            "Inter",
            "Poppins",
            "Roboto",
            "sans-serif",
        ].join(","),

        h1: {
            fontSize: "4rem",
            fontWeight: 800,
            letterSpacing: "-2px",
        },

        h2: {
            fontSize: "3rem",
            fontWeight: 700,
            letterSpacing: "-1px",
        },

        h3: {
            fontSize: "2.25rem",
            fontWeight: 700,
        },

        h4: {
            fontSize: "1.8rem",
            fontWeight: 700,
        },

        h5: {
            fontSize: "1.35rem",
            fontWeight: 600,
        },

        h6: {
            fontSize: "1.1rem",
            fontWeight: 600,
        },

        body1: {
            fontSize: "1rem",
            lineHeight: 1.8,
            color: "#4B5563",
        },

        body2: {
            fontSize: ".95rem",
            color: "#6B7280",
        },

        button: {
            textTransform: "none",
            fontWeight: 600,
            fontSize: "1rem",
        },
    },

    components: {
        MuiCssBaseline: {
            styleOverrides: {
                body: {
                    background:
                        "linear-gradient(180deg,#F8FAFC 0%,#EEF2FF 100%)",
                },
            },
        },

        MuiAppBar: {
            styleOverrides: {
                root: {
                    background: "rgba(255,255,255,.85)",
                    backdropFilter: "blur(18px)",
                    color: "#111827",
                    boxShadow: "0 8px 30px rgba(15,23,42,.06)",
                    borderBottom: "1px solid #E5E7EB",
                },
            },
        },

        MuiPaper: {
            styleOverrides: {
                root: {
                    borderRadius: 22,
                    boxShadow: "0 20px 50px rgba(15,23,42,.08)",
                    backgroundImage: "none",
                },
            },
        },

        MuiCard: {
            styleOverrides: {
                root: {
                    borderRadius: 24,
                    boxShadow: "0 20px 50px rgba(15,23,42,.08)",
                    transition: ".35s ease",

                    "&:hover": {
                        transform: "translateY(-6px)",
                        boxShadow: "0 30px 60px rgba(37,99,235,.12)",
                    },
                },
            },
        },

        MuiButton: {
            defaultProps: {
                disableElevation: true,
            },

            styleOverrides: {
                root: {
                    borderRadius: 14,
                    padding: "12px 26px",
                    fontWeight: 600,
                    transition: ".3s",

                    "&:hover": {
                        transform: "translateY(-2px)",
                    },
                },

                contained: {
                    background:
                        "linear-gradient(135deg,#2563EB,#7C3AED)",

                    "&:hover": {
                        background:
                            "linear-gradient(135deg,#1D4ED8,#6D28D9)",
                    },
                },

                outlined: {
                    borderWidth: 2,

                    "&:hover": {
                        borderWidth: 2,
                    },
                },
            },
        },

        MuiTextField: {
            defaultProps: {
                fullWidth: true,
                variant: "outlined",
                margin: "normal",
            },
        },

        MuiOutlinedInput: {
            styleOverrides: {
                root: {
                    borderRadius: 14,

                    "& fieldset": {
                        borderColor: "#D1D5DB",
                    },

                    "&:hover fieldset": {
                        borderColor: "#2563EB",
                    },

                    "&.Mui-focused fieldset": {
                        borderWidth: 2,
                        borderColor: "#2563EB",
                    },
                },
            },
        },

        MuiContainer: {
            defaultProps: {
                maxWidth: "xl",
            },
        },

        MuiChip: {
            styleOverrides: {
                root: {
                    borderRadius: 10,
                    fontWeight: 600,
                },
            },
        },
    },
});

export default theme;