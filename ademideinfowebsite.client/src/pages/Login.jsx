import { useState } from "react";
import {
    Alert,
    Box,
    Button,
    CircularProgress,
    Container,
    Paper,
    TextField,
    Typography,
} from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import useAuth from "../context/useAuth";
import { apiClient, unwrapResponse } from "../api/client"; 

export default function Login() {
    const navigate = useNavigate();
    const { login } = useAuth();

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    const [form, setForm] = useState({
        email: "",
        password: "",
    });

    const handleChange = (e) => {
        setForm((prev) => ({
            ...prev,
            [e.target.name]: e.target.value,
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        setError("");
        setLoading(true);

        try {
            const response = await apiClient.post("/Profile/login", form);

            const result = unwrapResponse(response);

            if (result.status) {
                login(
                    result.data.profileDetails,
                    result.data.token
                );

                navigate("/dashboard");
            } else {
                setError(result.message);
            }
        } catch (err) {
            setError(
                console.log(err) ||
                err.response?.data?.message ||
                "Unable to login."
            );
        } finally {
            setLoading(false);
        }
    };

    return (
        <Container maxWidth="sm">
            <Box
                sx={{
                    minHeight: "100vh",
                    display: "flex",
                    alignItems: "center",
                }}
            >
                <Paper
                    elevation={4}
                    sx={{
                        width: "100%",
                        p: 4,
                        borderRadius: 3,
                    }}
                >
                    <Typography
                        variant="h4"
                        align="center"
                        gutterBottom
                    >
                        Login
                    </Typography>

                    <Typography
                        align="center"
                        color="text.secondary"
                        mb={3}
                    >
                        Sign in to continue.
                    </Typography>

                    {error && (
                        <Alert severity="error" sx={{ mb: 2 }}>
                            {error}
                        </Alert>
                    )}

                    <Box component="form" onSubmit={handleSubmit}>
                        <TextField
                            fullWidth
                            margin="normal"
                            label="Email"
                            name="email"
                            type="email"
                            value={form.email}
                            onChange={handleChange}
                            required
                        />

                        <TextField
                            fullWidth
                            margin="normal"
                            label="Password"
                            name="password"
                            type="password"
                            value={form.password}
                            onChange={handleChange}
                            required
                        />

                        <Button
                            type="submit"
                            variant="contained"
                            fullWidth
                            size="large"
                            sx={{ mt: 3 }}
                            disabled={loading}
                        >
                            {loading ? (
                                <CircularProgress size={24} color="inherit" />
                            ) : (
                                "Login"
                            )}
                        </Button>

                        <Button
                            component={Link}
                            to="/register"
                            fullWidth
                            sx={{ mt: 2 }}
                        >
                            Don't have an account? Register
                        </Button>
                    </Box>
                </Paper>
            </Box>
        </Container>
    );
}