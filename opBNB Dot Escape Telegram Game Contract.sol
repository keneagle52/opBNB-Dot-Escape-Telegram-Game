// SPDX-License-Identifier: MIT
pragma solidity ^0.8.17;

// Define the "DotEscapeGame" contract
contract DotEscapeGame {
    // Mapping to track dot points for each player
    mapping(address => uint256) private dotPoints;

    // Events
    event DotPointsIncreased(address indexed player, uint256 currentPoints);

    // Modifier to ensure valid address
    modifier validAddress(address player) {
        require(player != address(0), "Invalid player address");
        _;
    }

    // Function to increase dot points for a player
    function increaseDotPoints(address player) external validAddress(player) {
        dotPoints[player] += 1;

        emit DotPointsIncreased(player, dotPoints[player]);
    }

    // Function to retrieve a player's dot points
    function getDotPoints(address player) external view returns (uint256) {
        return dotPoints[player];
    }
}
