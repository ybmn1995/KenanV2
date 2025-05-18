#include <fstream>
#include <string>

std::string ReadVMPassword() {
    std::ifstream file(".env");
    std::string line;
    while (std::getline(file, line)) {
        if (line.find("VMPassword=") == 0) {
            return line.substr(strlen("VMPassword="));
        }
    }
    return "";
}
