import { Box, Typography } from "@mui/material";

export default function Footer() {
    return (
        <Box
            sx={{
                py: 2,
                textAlign: "center",
                bgcolor: "white",
                borderTop: "1px solid #ddd",
            }}
        >
            <Typography variant="body2">
                © {new Date().getFullYear()} Ademide Website.
            </Typography>
        </Box>
    );
}